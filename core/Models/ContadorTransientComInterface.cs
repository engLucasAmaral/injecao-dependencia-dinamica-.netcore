using core.InjectionDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public interface IContadorTransientComInterface
    {
        void Incrementar();
        int GetValorAtual();
        string GetGuid();

    }

    [Transient(Interface = typeof(IContadorTransientComInterface))]
    public class ContadorTransientComInterface : IContadorTransientComInterface
    {
        public string _guid { get; private set; }
        public int _valorAtual { get; set; }

        public ContadorTransientComInterface()
        {
            this._valorAtual = 0;
            this._guid = Guid.NewGuid().ToString();
        }

        public void Incrementar()
        {
            _valorAtual++;
        }

        public int GetValorAtual()
        {
            return _valorAtual;
        }
        public string GetGuid()
        {
            return _guid;
        }

    }
}
