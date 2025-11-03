# FASE 1 - CONCEITUAÇÃO

Fase 1: Conceituação (Sem Código) 

O processamento de pedidos segue um ritual comum composto por três 
etapas principais: validação, cálculo do total e emissão do recibo. Apesar dessa 
sequência ser a mesma para todos os pedidos, a forma de execução varia 
conforme o tipo de pedido. 

Nos Pedidos Nacionais, as diferenças estão na aplicação de impostos 

internos (como ICMS e IPI)  e na emissão de documentos  fiscais locais, como a 
Nota Fiscal eletrônica (NF-e). Já nos Pedidos Internacionais,  entram fatores 
adicionais como taxas de importação, custos aduaneiros, conversão cambial e 
a emissão de um documento  específico de exportação, o Commercial Invoice. 

Dessa forma, a herança por especialização é usada para representar  os 
tipos de pedido, pois ambos compartilham o mesmo fluxo  geral, mas redefinem 
comportamentos  específicos conforme suas regras fiscais e comerciais. 

Por outro lado, elementos  como frete, embalagem, seguro e promoção 
são políticas independentes  e combináveis. Para manter  o sistema flexível  e 
com baixo acoplamento,  essas políticas devem ser implementadas por 
composição, permitindo que sejam adicionadas, removidas ou substituídas  sem 
alterar a estrutura  principal dos pedidos. 
