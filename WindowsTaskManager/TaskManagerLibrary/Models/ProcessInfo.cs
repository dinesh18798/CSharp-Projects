namespace TaskManagerLibrary.Models
{
    /// <summary>
    /// The <c>ProcessInfo</c> class.
    /// Contains all properties of the process
    /// </summary>
    public class ProcessInfo
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public int ThreadCount { get; set; }

        #endregion
    }
}
