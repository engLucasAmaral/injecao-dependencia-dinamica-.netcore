# Injeção de depêndencia de forma dinamica com .net core
Uma forma simples e menos verbosa para implementar a injeção de dependencia com .net core!

Para funcionar dinamicamente é necessario anotar as classes que serão usadas pelo DI no projeto com:

# [RequestScoped]
    As classes com essa anotação serão adicionadas ao DI como RequestScoped
# [Transient]
    As classes com essa anotação serão adicionadas ao DI como Transient
# [Singleton]
    As classes com essa anotação serão adicionadas ao DI como Singleton


No exemplo, as classes de DI estão no projeto core que é referenciado no projeto api (projeto principal). 

a) É necessário adicionar no startup do projeto principal "api", a injeção de dependência de forma dinamica.
		Observar o metodo: AdicionaInjecaoDependencia
