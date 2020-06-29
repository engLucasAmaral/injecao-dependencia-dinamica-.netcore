# Injeção de dependência de forma dinâmica com .netcore
Uma forma simples e menos verbosa para implementar a injeção de dependência com .netcore!

Para funcionar dinamicamente é necessario anotar as classes que serão usadas pelo container DI no projeto com:

# [RequestScoped]
    As classes com essa anotação serão adicionadas ao container DI como RequestScoped
# [Transient]
    As classes com essa anotação serão adicionadas ao container DI como Transient
# [Singleton]
    As classes com essa anotação serão adicionadas ao container DI como Singleton

# [RequestScoped(Interface = typeof(IContadorComInterface))]
# [Transient(Interface = typeof(IContadorComInterface))]
# [Singleton(Interface = typeof(IContadorComInterface))]
    Injeção com Interfaces...

No exemplo, as classes de DI estão no projeto core que é referenciado no projeto api (projeto principal). 

a) É necessário adicionar no startup do projeto principal "api", a injeção de dependência de forma dinâmica.
		Observar o metodo: AdicionaInjecaoDependencia

## Testes: 

Será retornado um contador e um guid, informando a instancia da classe!
 
http://localhost:5000/api/requestscoped

http://localhost:5000/api/transient

http://localhost:5000/api/singleton

http://localhost:5000/api/transientcominterface




# Swagger

http://localhost:5000/api/v1/swagger/index.html
