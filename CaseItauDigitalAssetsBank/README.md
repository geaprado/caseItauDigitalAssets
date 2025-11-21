API - Case Itau Digital Assets.

Api desenvolvida com o objetivo de refatorar a versão em node.js disponibilizada pela comunidade de Digital Assets.

Foi proposto o seguinte:

1-Codificação
	-Faça o download do projeto na sua máquina. Não realize commits na branch main e não crie novas branchs.
	O código da api de clientes faz mal uso dos objetos, não segue boas práticas e não possui qualidade. Refatore o código utilizando as melhores bibliotecas, 
	práticas, patterns e garanta a qualidade da aplicação. Fique à vontade para mudar o que achar necessário.

	-O controle de saldo do cliente possui um erro. Identifique e implemente a correção.

	Erros identificados:
		A api disponibilizada não possuia documentação, testes e nem controle de transações.
		Permitia realizar saque mesmo sem saldo.



	Para nós segurança e qualidade é um tema sério, implemente as ações que achar prudente para garantir estes requisitos
	Utilizando o angular, crie uma aplicação web que consuma todos os métodos da API de clientes

2-Desenho de solução
	-Considere que o produto da etapa 1 está crescendo e precisa escalar, com previsão de ter 5 mil usuários acessando simultaneamente o produto
	Proponha um desenho de arquitetura de solução para que esse produto consiga suportar essa escala
	O desenho de arquitetura precisa considerar o uso da cloud AWS como infraestrutura
	Você tem cheque em branco para o que entender se o melhor para essa aplicação

