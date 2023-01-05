using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Authorization
{   
    public enum ResourceOperation
    {
        Create, 
        Read,
        Update, 
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation Operation { get;}
        public ResourceOperationRequirement(ResourceOperation operation)
        {
            Operation = operation;
        }
    }
}
