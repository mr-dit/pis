using pis.Repositorys;
using pis_web_api.Models.db;

namespace pis_web_api.Services
{
    public class RolesService
    {
        private static Lazy<Role> kurator_vetservice = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Куратор ВетСлужбы"));
        private static Lazy<Role> kurator_trapping = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Куратор по отлову"));
        private static Lazy<Role> kurator_shelter = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Куратор приюта"));
        private static Lazy<Role> operator_vetservice = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Оператор ВетСлужбы"));
        private static Lazy<Role> operator_trapping = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Оператор по отлову"));
        private static Lazy<Role> signer_vetservice = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Подписант ВетСлужбы"));
        private static Lazy<Role> signer_trapping = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Подписант по отлову"));
        private static Lazy<Role> signer_shelter = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Подписант приюта"));
        private static Lazy<Role> kurator_omsu = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Куратор ОМСУ"));
        private static Lazy<Role> operator_omsu = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Оператор ОМСУ"));
        private static Lazy<Role> signer_omsu = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Подписант ОМСУ"));
        private static Lazy<Role> operator_shelter = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Оператор приюта"));
        private static Lazy<Role> doctor = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Ветврач"));
        private static Lazy<Role> doctor_shelter = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Ветврач приюта"));
        private static Lazy<Role> admin = new Lazy<Role>(()
            => RoleRepository.GetRoleByName("Админ"));

        public static Role KURATOR_VETSERVICE => kurator_vetservice.Value;
        public static Role KURATOR_TRAPPING => kurator_trapping.Value;
        public static Role KURATOR_SHELTER => kurator_shelter.Value;
        public static Role OPERATOR_VETSERVICE => operator_vetservice.Value;
        public static Role OPERATOR_TRAPPING => operator_trapping.Value;
        public static Role SIGNER_VETSERVICE => signer_vetservice.Value;
        public static Role SIGNER_TRAPPING => signer_trapping.Value;
        public static Role SIGNER_SHELTER => signer_shelter.Value;
        public static Role KURATOR_OMSU => kurator_omsu.Value;
        public static Role OPERATOR_OMSU => operator_omsu.Value;
        public static Role SIGNER_OMSU => signer_omsu.Value;
        public static Role OPERATOR_SHELTER => operator_shelter.Value;
        public static Role DOCTOR => doctor.Value;
        public static Role DOCTOR_SHELTER => doctor_shelter.Value;
        public static Role ADMIN => admin.Value;
    }
}
