namespace Chinchillada.Behavior
{
    using Sirenix.Serialization;
    using System.Collections;
    using Sirenix.OdinInspector;

    public class ExecutableComponent : ChinchilladaBehaviour, IExecutable
    {
        [OdinSerialize, Required] private IExecutable executable;

        public IEnumerator Execute()
        {
            return this.executable.Execute();
        }
    }
}