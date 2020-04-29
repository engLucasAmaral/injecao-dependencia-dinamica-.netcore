using System;
using core.InjectionDependency;

namespace core.Models
{

    [Singleton]
    public class ContadorSingleton
    {
       public string _guid  {get; private set;}
       public int _valorAtual {get;set;}

       public ContadorSingleton(){
           this._valorAtual = 0;
           this._guid  = Guid.NewGuid().ToString();
       }

       public void Incrementar()
        {
            _valorAtual++;
        } 
    }
}
