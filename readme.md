# ELK no docker

## Sobre

**Esse ambiente está configurado para facilitar o desenvolvimento, não invente de usar ele assim em produção!**

### Em linhas gerais

A aplicação .Net está com o pacote do serilog configurado para enviar os logs em formato json para o RabbitMq.

Sempre que o ILogger (ou ILogger<>) for injetado os logs serão direcionados para a exchange que foi configurada no appsetting.json.

O Logstash vai pegas as mensagens do Rabbit e grava-las no Elastic Search.

O Kibana é a interface que permite a visualização dos logs.
Ele já está apontando para o Elastic Search, mas não configurei nenhum dashboard. Você pode fazer isso manualmente quando quiser usar.



### Iniciando o ambiente
Considerando que o docker esteja instalado, basta ir até a pasta do repositório executar o seguinte comando
```
docker compose up
```

### Fuçando
Após todos os containers estarem de pé...

A aplicação pode ser acessada pelo endereço abaixo, ela vai ter só um swagger mesmo.

http://localhost:18080

A interface de gerenciamento do RabbitMq está com o usuário guest (senha guest) ativo.

http://localhost:15672

O Kibana está no seguinte local, você pode usar o menu Analytis -> Discover para configurar uma exibição.

http://localhost:5601

As portas do RabbitMq e o Elastic Search são 5672 e 9200 respectivamente, caso queira fazer alguma outra conexão.

Para entender como cada aplicação está configurada, sugiro começar olhando o docker-compose pela ordem que os containers aparecem, arquivos auxiliares de cara aplicação podem ser encontrados nas suas pastas.

## Links úteis

#### Imagem do RabbitMq
https://hub.docker.com/_/rabbitmq

#### Exportação de configurações do RabbitMq
https://www.rabbitmq.com/definitions.html

#### Guia de instalação do Elastic Search
https://www.elastic.co/guide/en/elasticsearch/reference/8.5/docker.html

#### Guia de instalação do Log Stash
https://www.elastic.co/guide/en/logstash/8.5/docker-config.html

#### Guia de instalação do Kibana
https://www.elastic.co/guide/en/kibana/8.5/docker.html