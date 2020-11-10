using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DFS.Models;

namespace DFS
{
	public class TodoItemManager
	{
		IRestService restService;

		public TodoItemManager (IRestService service)
		{
			restService = service;
		}

        public Task<String> SignUp(Models.TraineeSignupModel signupModel){
            return restService.SignUpAsync(signupModel);
        }

        public Task<String> Login(Models.LoginRequestModel loginRequestModel)
        {
            return restService.LoginAsync(loginRequestModel);
        }

        public Task<Models.TrainerListModel> FetchTrainerList()
        {
            return restService.FetchTrainerList();
        }

        public Task<String> GetFacebookInfo()
        {
            return restService.GetFacebookInfo();
        }

        public Task<String> GetInstagramInfo(string accessToken)
        {
            return restService.GetInstagramInfo(accessToken);
        }

        public Task<String> GetInstaToken(string accessToken)
        {
            return restService.GetInstaToken(accessToken);
        }

        public Task<String> GetInstagramMedia(string accessToken)
        {
            return restService.GetInstagramMedia(accessToken);
        }

        public Task<String> UpdateInstagramMedia(string accessToken)
        {
            return restService.UpdateInstagramMedia(accessToken);
        }

        public Task<SetTimeSlotResponseModel> SetTimeSlot(Models.SetTimeSlotsRequestModel setTimeSlots)
        {
            return restService.SetCalenderEvent(setTimeSlots);
        }

        public Task<GetTimeSlotResponse> GetTimeSlots(GetTimeSlotRequest getTimeSlotRequest)
        {
            return restService.GetTimeSlots(getTimeSlotRequest);
        }

        public Task<PaymentResponse> StartPayment(Models.PaymentRequest paymentrequest)
        {
            return restService.StartPayment(paymentrequest);
        }

        public Task<String> SubmitRating(RatingRequestModel ratingRequestModel)
        {
            return restService.SubmitRating(ratingRequestModel);
        }

        public Task<String> CreateOtpService(CreateOtpModel createOtpModel)
        {
            return restService.CreateOtpService(createOtpModel);
        }

        public Task<String> SubmitOtpService(SubmitOtpModel submitOtpModel)
        {
            return restService.SubmitOtpService(submitOtpModel);
        }

        public Task<String> DeleteInstaImages()
        {
            return restService.DeleteInstaImages();
        }
    }
}
