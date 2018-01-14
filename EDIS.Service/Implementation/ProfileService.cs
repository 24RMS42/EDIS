using System;
using System.Linq;
using System.Threading.Tasks;
using EDIS.Core;
using EDIS.Data.Api.Base.Interfaces;
using EDIS.Domain.Profile;
using EDIS.DTO.Requests;
using EDIS.DTO.Responses;
using EDIS.Service.Base;
using EDIS.Service.Interfaces;
using EDIS.Shared.Models;

namespace EDIS.Service.Implementation
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly IRequestProvider _requestProvider;

        public async Task<User> GetProfile(string email = "")
        {
            if (email != "")
            {
                var user = await DbManager.UserRepository.FindByQuery(x => x.UserEmail == email);
                if (user != null)
                    return user;
            }

            var url = GlobalSettings.BaseURL + "/profile";

            var response = await _requestProvider.PostAsync<BaseRequest, ProfileResponse>(url, new BaseRequest { Token = Settings.AccessToken });

            if(response?.Users == null)
                return new User();

            if (response.Users.Any())
            {
                User user = response.Users.FirstOrDefault();
                if (user != null) {
                    var userOld = await DbManager.UserRepository.FindByQuery(x => x.UserId == user.UserId);
                    if (userOld != null)
                    {
                        await DbManager.UserRepository.DeleteByQuery(x => x.UserId == user.UserId);
                    }

                    await DbManager.UserRepository.Add(user);

                    if (response.Instruments.Any())
                    {
                        Instrument instrument = response.Instruments.FirstOrDefault();
                        if (instrument != null)
                        {
                            var instrumentOld = await DbManager.InstrumentRepository.FindByQuery(x => x.UserId == instrument.UserId);
                            if (instrumentOld != null)
                            {
                                await DbManager.InstrumentRepository.DeleteByQuery(x => x.UserId == instrument.UserId);
                            }

                            await DbManager.InstrumentRepository.Add(instrument);

                        }
                    }
                }
            }

            return response.Users.FirstOrDefault();
        }


        public async Task<ServiceResult> GetProfileDetails(string userId)
        {
            var userDb = await DbManager.UserRepository.GetUser(userId);
            var instrumentDb = await DbManager.InstrumentRepository.GetInstrument(userId);
            if (userDb != null)
            {
                //var basicInfo = AutoMapper.Mapper.Map<CertificateBasicInfo>(certificateDb);
                var user = userDb;
                var instrument = instrumentDb ?? new Instrument();
                var userLogoPath = await DbManager.UserRepository.GetUserLogoImageWithPath(user);
                var editProfile = new EditProfile
                {
                    User = user,
                    Instrument = instrument,
                    UserLogoPath = userLogoPath
                };

                return new SuccessServiceResult { ResultObject = editProfile };
            }


            return new FalseServiceResult("Response is null");
            
        }

        public Task<Instrument> GetInstrument(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> SaveUserBasicInfo(User editedUser)
        {
            var userOld = await DbManager.UserRepository.GetUser(editedUser.UserId);
            var user = MapBasicInfoToUser(userOld, editedUser);
            await DbManager.UserRepository.UpdateUser(user);

            return new SuccessServiceResult();
        }

        public async Task<ServiceResult> SaveUserElectricianInfo(User editedUser, Instrument editedInstrument)
        {
            var userOld = await DbManager.UserRepository.GetUser(editedUser.UserId);
            var instrumentOld = await DbManager.InstrumentRepository.GetInstrument(editedInstrument.UserId);
            var user = MapUserInfoToUser(userOld, editedUser);
            var instrument = MapInstrumentInfoToInstrument(instrumentOld, editedInstrument);
            await DbManager.UserRepository.UpdateUser(user);
            await DbManager.InstrumentRepository.UpdateInstrument(instrument);

            return new SuccessServiceResult();
        }

        public ProfileService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        private User MapBasicInfoToUser(User user, User editedUser)
        {
            user.UserFullname = editedUser.UserFullname;
            user.UserOrganisation = editedUser.UserOrganisation;
            user.UserPosition = editedUser.UserPosition;
            user.UserAddress = editedUser.UserAddress;
            user.UserMobile = editedUser.UserMobile;
            user.UserLandline = editedUser.UserLandline;
            user.UserWebsite = editedUser.UserWebsite;
            user.UserSubscribedRpts = editedUser.UserSubscribedRpts;
            
            return user;
        }

        private User MapUserInfoToUser(User user, User editedUser)
        {
            user.ContractorAccreditedCertificationBody = editedUser.ContractorAccreditedCertificationBody;
            user.ContractorAccreditationNumber = editedUser.ContractorAccreditationNumber;
            user.UserBranchNumber = editedUser.UserBranchNumber;

            return user;
        }

        private Instrument MapInstrumentInfoToInstrument(Instrument instrument, Instrument editedInstrument)
        {
            instrument.InstrumentEfliSn = editedInstrument.InstrumentEfliSn;
            instrument.InstrumentIrSn = editedInstrument.InstrumentIrSn;
            instrument.InstrumentCSn = editedInstrument.InstrumentCSn;
            instrument.InstrumentRcdSn = editedInstrument.InstrumentRcdSn;
            instrument.InstrumentOSn1 = editedInstrument.InstrumentOSn1;
            instrument.InstrumentOSn2 = editedInstrument.InstrumentOSn2;
            
            return instrument;
        }


    }
}