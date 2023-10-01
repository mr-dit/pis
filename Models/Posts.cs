namespace pis.Models
{
    public class Posts
    {
        public static Post TutorVet { get; } = new Post(1, "TutorVet");
        public static Post TutorTrapping { get; } = new Post(2, "TutorTrapping");
        public static Post TutorShelter { get; } = new Post(3, "TutorShelter");
        public static Post OperatorVet { get; } = new Post(4, "OperatorVet");
        public static Post OperatorTrapping { get; } = new Post(5, "OperatorTrapping");
        public static Post SignerVet { get; } = new Post(6, "SignerVet");
        public static Post SignerTrapping { get; } = new Post(7, "SignerTrapping");
        public static Post SignerShelter { get; } = new Post(8, "SignerShelter");
        public static Post TutorOMSU { get; } = new Post(9, "TutorOMSU");
        public static Post OperatorOMSU { get; } = new Post(10, "OperatorOMSU");
        public static Post SignerOMSU { get; } = new Post(11, "SignerOMSU");
        public static Post OperatorShelter { get; } = new Post(11, "OperatorShelter");
        public static Post Doctor { get; } = new Post(12, "Doctor");
        public static Post DoctorVet { get; } = new Post(13, "DoctorVet");
    }
}
