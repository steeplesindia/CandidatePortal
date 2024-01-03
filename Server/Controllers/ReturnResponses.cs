using CandidatePortal.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidatePortal.Server.Controllers
{
    public class ReturnResponses : ControllerBase
    {
        public async Task<IActionResult> ReturnResponseDataAsync(Task<ResponseStatusData> response)
        {
            return response.Result.StatusCode switch
            {
                200 => Ok(response.Result),
                204 => NoContent(),
                400 => BadRequest(response.Result),
                401 => Unauthorized(response.Result),
                404 => NotFound(response.Result),
                500 => StatusCode(500, "Internal Server Error. Something went Wrong!"),
                501 => StatusCode(501, "Service Unavailable"),
                _ => NotFound(response.Result),
            };
        }

        public IActionResult ReturnResponseData(ResponseStatusData response)
        {
            return response.StatusCode switch
            {
                200 => Ok(response),
                204 => NoContent(),
                400 => BadRequest(response),
                401 => Unauthorized(response),
                404 => NotFound(response),
                500 => StatusCode(500, "Internal Server Error. Something went Wrong!"),
                501 => StatusCode(501, "Service Unavailable"),
                _ => NotFound(response),
            };
        }
    }
}
