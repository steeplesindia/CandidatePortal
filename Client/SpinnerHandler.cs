﻿namespace CandidatePortal.Client
{
    public class SpinnerHandler : DelegatingHandler
    {
        private readonly SpinnerService _spinnerService;

        public SpinnerHandler(SpinnerService spinnerService)
        {
            _spinnerService = spinnerService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _spinnerService.Show();
            //await Task.Delay(3000); // artificial delay for testing
            var response = await base.SendAsync(request, cancellationToken);
            _spinnerService.Hide();
            return response;
        }
    }
}
