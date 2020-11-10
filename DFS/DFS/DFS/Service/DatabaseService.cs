using System;
using System.Threading.Tasks;
using DFS.Models;
using Xamarin.Forms;

namespace DFS.Service
{
    public class DatabaseService : IRestService
    {
        public DatabaseService()
        {

        }

        public LoginResponse.SyncLoginResponse LoginResponse(string SelectedInput)
        {
            throw new NotImplementedException();
        }



        // Not Implemented
        public Task<string> LoginAsync(LoginRequestModel loginRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> SignUpAsync(TraineeSignupModel signupModel)
        {
            throw new NotImplementedException();
        }

        public Task<TrainerListModel> FetchTrainerList()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetFacebookInfo()
        {
            throw new NotImplementedException();
        }

        public Task<string> SetCalenderEvent(SetTimeSlotsRequestModel setTimeSlots)
        {
            throw new NotImplementedException();
        }

        Task<SetTimeSlotResponseModel> IRestService.SetCalenderEvent(SetTimeSlotsRequestModel setTimeSlots)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> StartPayment(PaymentRequest paymentRequest)
        {
            throw new NotImplementedException();
        }

        public Task<GetTimeSlotResponse> GetTimeSlots(GetTimeSlotRequest getTimeSlotRequest)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetInstagramInfo(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetInstagramMedia(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> SubmitRating(RatingRequestModel ratingRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateInstagramMedia(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateOtpService(CreateOtpModel createOtpModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> SubmitOtpService(SubmitOtpModel submitOtpModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteInstaImages()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetInstaToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
