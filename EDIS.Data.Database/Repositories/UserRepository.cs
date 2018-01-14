using EDIS.Data.Database.Repositories.BaseRepository;
using EDIS.Data.Database.Repositories.Interfaces;
using EDIS.Domain.Profile;
using EDIS.Shared.Helpers;
using System;
using System.Threading.Tasks;
using EDIS.Core;
using System.Text.RegularExpressions;
using SQLiteNetExtensionsAsync.Extensions;

namespace EDIS.Data.Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public async new Task Add(User user)
        {
            if (!string.IsNullOrEmpty(user.UserLogo) && !string.IsNullOrEmpty(user.UserLogoContent)) {
                try
                {
                    //var base64Image = user.UserLogoContent.Replace(/^ data:image\/[a - z] +; base64,/, "");


                    /*var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)");

                    var match = regex.Match(user.UserLogoContent);

                    var mime = match.Groups["mime"].Value;
                    var encoding = match.Groups["encoding"].Value;*/
                    var imgData = user.UserLogoContent.Substring(user.UserLogoContent.IndexOf(",") + 1);

                    byte[] imageBytes = Convert.FromBase64String(imgData);
                    var folder = await PCLHelper.GetFolder(Settings.profileFolder);
                    if (folder != null)
                    {
                        //var logofile = await PCLHelper.CreateFile(user.UserLogo, folder);
                        var logofileName = user.UserLogo.Replace('-', '_');
                        var logofile = await PCLHelper.WriteTextAllAsync(logofileName, imageBytes.ToString(), folder);
                        //await logofile.WriteAllTextAsync(content);
                    }
                }
                catch (Exception e)
                {
                    // skip the image
                }
            }

            user.UserLogoContent = "";
            await Connection.InsertOrReplaceAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            var userOld = await GetUser(user.UserId);
            if (userOld != null)
            {
                await Connection.UpdateWithChildrenAsync(user);
            }
        }

        public async Task<string> GetUserLogoImageWithPath(User user)
        {
            try
            {
                var folder = await PCLHelper.GetFolder(Settings.profileFolder);
                if (folder != null && user != null && !string.IsNullOrEmpty(user.UserLogo))
                {
                    var logofileName = user.UserLogo.Replace('-', '_');
                    var filePath = await PCLHelper.GetFilePathAsync(logofileName, folder);
                    if(filePath != null)
                    {
                        return filePath.ToString();
                    }
                }
                return null;
            }catch(Exception e)
            {
                return null;
            }
        }

        public async Task<User> GetUser(string userId)
        {
            try
            {
                var user = await Connection.Table<User>().Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (user != null)
                    return await Connection.GetWithChildrenAsync<User>(userId);
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}