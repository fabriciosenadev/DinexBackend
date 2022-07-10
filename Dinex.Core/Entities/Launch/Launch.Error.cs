namespace Dinex.Core
{
    public partial class Launch
    {
        public enum Error 
        { 
            ErrorToCreateLaunch,
            ErrorToUpdateLaunch,
            ErrorToDeleteLaunch,

            LaunchNotFound,
            HasLaunchWithCategory,
        }
    }
}
