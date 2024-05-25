using AppLoginCore_M.Libraries.Login;
using AppLoginCore_M.Models.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppLoginCore_M.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private string _tipoColaboradorAuth;
        LoginColaborador _loginColaborador;
        public ColaboradorAutorizacaoAttribute(string tipoColaboradorAuth = ColaboradorTipoConstant.Comum)
        {
            _tipoColaboradorAuth=tipoColaboradorAuth;
        }        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            Models.Colaborador colaborador = _loginColaborador.GetColaborador();
            if(colaborador == null)
            {
                context.Result = new RedirectToActionResult("login", "Home", null);
            }
            else
            {
                if (colaborador.Tipo == ColaboradorTipoConstant.Comum && _tipoColaboradorAuth == ColaboradorTipoConstant.Gerente)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
