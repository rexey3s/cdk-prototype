using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Abp.Dependency;
using Newtonsoft.Json;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;


namespace ServiceLayer.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ActivityAuthorize : Attribute
    {
        public const string ExecuteDeltaView = "UC_ExecuteDelta_View";
        public const string ExecuteDeltaSelectTrainee = "UC_ExecuteDelta_SelectTrainee";
        public const string TrainingManagementView = "UC_TrainingManagement_View";
        public const string TrainingManagementNew = "UC_TrainingManagement_New";
        public const string TrainingManagementComplete = "UC_TrainingManagement_Complete";
        public const string TrainingManagementUpdate = "UC_TrainingManagement_Update";
        public const string TrainingManagementCancel = "UC_TrainingManagement_Cancel";
        public const string ProgramManagementView = "UC_ProgramManagement_View";
        public const string JobFunctionManagementView = "UC_JobFunctionManagement_View";
        public const string UserManagementView = "UC_UserManagement_View";
        public const string ExecuteDeltaExecute = "UC_ExecuteDelta_Execute";
        public string ActivityName { get; set; }

        public ActivityAuthorize()
        {
            ActivityName = ""; // Default value for authentication 
        }
        public ActivityAuthorize(string activityName)
        {
            ActivityName = activityName;
        }
    }
}