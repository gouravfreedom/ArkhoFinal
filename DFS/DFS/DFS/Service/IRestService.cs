using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DFS.Models;

namespace DFS
{
	public interface IRestService
	{
	
        Task<String> SignUpAsync(Models.TraineeSignupModel signupModel);

        Task<String> LoginAsync(Models.LoginRequestModel loginRequestModel);

        Task<Models.TrainerListModel> FetchTrainerList();

        Models.LoginResponse.SyncLoginResponse LoginResponse(String SelectedInput);

        Task<string> GetFacebookInfo();

        Task<string> GetInstagramInfo(string accessToken);

        Task<string> GetInstagramMedia(string accessToken);

        Task<string> UpdateInstagramMedia(string accessToken);

        Task<SetTimeSlotResponseModel> SetCalenderEvent(Models.SetTimeSlotsRequestModel setTimeSlots);

        Task<PaymentResponse> StartPayment(Models.PaymentRequest paymentRequest);

        Task<GetTimeSlotResponse> GetTimeSlots(GetTimeSlotRequest getTimeSlotRequest);

        Task<string> SubmitRating(RatingRequestModel ratingRequestModel);

        Task<string> CreateOtpService(CreateOtpModel createOtpModel);

        Task<string> SubmitOtpService(SubmitOtpModel submitOtpModel);

        Task<string> DeleteInstaImages();

        Task<string> GetInstaToken(String token);
    }
}
