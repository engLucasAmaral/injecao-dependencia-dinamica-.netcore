using System;
using core.InectionDependency;

namespace core.Models
{

    [RequestScoped]
    public class ContadorRequestScoped
    {
       public string _guid  {get; private set;}
       public int _valorAtual {get;set;}

       public ContadorRequestScoped(){
           this._valorAtual = 0;
           this._guid  = Guid.NewGuid().ToString();
       }

       public void Incrementar()
        {
            _valorAtual++;
        } 
    }
}
