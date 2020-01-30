using ENode.Commanding;

namespace Registration.Web.Extensions
{
    public static class AsyncTaskResultExtensions
    {
        public static bool IsSuccess(this CommandResult result)
        {
            if (result.Status == CommandStatus.Failed)
            {
                return false;
            }
            return true;
        }
        public static string GetErrorMessage(this CommandResult result)
        {
            if (result.Status == CommandStatus.Failed)
            {
                return result.Result;
            }
            return null;
        }
    }
}