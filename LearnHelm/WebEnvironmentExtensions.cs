using Microsoft.AspNetCore.Hosting;
using System;

namespace LearnHelm
{
    public static class WebEnvironmentExtensions
    {
        internal const string DeploymentEnvironmentEnvVar = "DEPLOYMENT_ENVIRONMENT";
        internal const string KubernetesDeploymentTarget = "KUBERNETES";
        public static bool IsDeployedIn(this IWebHostEnvironment env, string deploymentTarget)
        {
            var deployementEnvironment = Environment.GetEnvironmentVariable(DeploymentEnvironmentEnvVar);
            if (string.IsNullOrWhiteSpace(deployementEnvironment)) return false;
            return string.Equals(deployementEnvironment, deploymentTarget, StringComparison.InvariantCulture);
        }
        public static bool IsDeployedInKubernetes(this IWebHostEnvironment env)
        {
            return env.IsDeployedIn(KubernetesDeploymentTarget);
        }
    }
}
