# Gerenciamento de Doações Fome Zero

## Objetivo
O projeto **Gerenciamento de Doações Fome Zero** visa desenvolver uma plataforma de doações alinhada aos Objetivos de Desenvolvimento Sustentável (ODS), facilitando a conexão entre doadores e beneficiários. A ideia é simplificar o processo de doação de dinheiro ou alimentos, permitindo que beneficiários registrem e escolham locais e horários para a retirada das doações.

## Funcionalidades Principais
### Configuração
- Cadastro de doadores e beneficiários.
- Definição de locais e horários disponíveis para retirada de doações.
- Integração com ONGs e instituições participantes para facilitar o recebimento de doações.

### Condução
- Sistema de login para doadores e beneficiários.
- Realização de doações via PIX ou doação de alimentos.
- Agendamento de retirada de doações por beneficiários.

### Análise
- Controle de retirada de doações, com análise da demanda por local e horário.

## Estrutura do Banco de Dados
Abaixo, uma breve descrição da estrutura do banco de dados utilizado neste projeto:

1. **Tabela `tipo_usuario`**: Armazena os tipos de usuários (Doador, Beneficiário, Admin).
2. **Tabela `usuarios`**: Armazena informações dos usuários (doadores, beneficiários e admin).
3. **Tabela `tipo_doacao`**: Armazena tipos de doações (dinheiro, alimentos, etc.).
4. **Tabela `doacoes`**: Armazena informações sobre as doações realizadas.
5. **Tabela `locais_retirada`**: Armazena locais e horários disponíveis para retirada de doações.
6. **Tabela `retirada_doacoes`**: Registra as retiradas de doações pelos beneficiários.
7. **Tabela `instituicoes`**: Armazena informações sobre ONGs e instituições participantes.
8. **Tabela `doacoes_instituicoes`**: Faz a ligação entre as doações e as instituições participantes.

## Script de Criação do Banco de Dados
Para criar o banco de dados necessário para o projeto, utilize o script SQL abaixo:

```sql
-- Tabela para armazenar tipos de usuários (Doador, Beneficiário, Admin)
CREATE TABLE tipo_usuario (
    id SERIAL PRIMARY KEY,
    descricao VARCHAR(50) NOT NULL UNIQUE
);

-- Inserir os tipos de usuário básicos
INSERT INTO tipo_usuario (descricao) VALUES ('Doador'), ('Beneficiário'), ('Admin');

-- Tabela para armazenar informações dos usuários (doadores, beneficiários e admin)
CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    telefone VARCHAR(15),
    senha VARCHAR(100) NOT NULL,
    tipo_usuario_id INT NOT NULL REFERENCES tipo_usuario(id),
    documento_identificacao VARCHAR(20) UNIQUE
);

-- Tabela para armazenar tipos de doações (dinheiro, alimentos, outros)
CREATE TABLE tipo_doacao (
    id SERIAL PRIMARY KEY,
    descricao VARCHAR(50) NOT NULL UNIQUE
);

-- Inserir os tipos de doação básicos
INSERT INTO tipo_doacao (descricao) VALUES ('Dinheiro'), ('Alimentos');

-- Tabela para armazenar informações sobre as doações
CREATE TABLE doacoes (
    id SERIAL PRIMARY KEY,
    usuario_id INT REFERENCES usuarios(id),
    tipo_doacao_id INT REFERENCES tipo_doacao(id),
    valor NUMERIC(10, 2),
    descricao_alimento VARCHAR(255),
    data_doacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabela para armazenar locais de retirada
CREATE TABLE locais_retirada (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    endereco VARCHAR(255) NOT NULL,
    horario_disponivel VARCHAR(50) NOT NULL
);

-- Tabela para registrar retiradas das doações pelos beneficiários
CREATE TABLE retirada_doacoes (
    id SERIAL PRIMARY KEY,
    doacao_id INT REFERENCES doacoes(id),
    beneficiario_id INT REFERENCES usuarios(id),
    local_retirada_id INT REFERENCES locais_retirada(id),
    data_agendada TIMESTAMP NOT NULL,
    data_retirada TIMESTAMP
);

-- Tabela para integrar ONGs e instituições participantes
CREATE TABLE instituicoes (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    contato VARCHAR(100)
);

-- Tabela para integrar doações com ONGs e instituições participantes
CREATE TABLE doacoes_instituicoes (
    id SERIAL PRIMARY KEY,
    doacao_id INT REFERENCES doacoes(id),
    instituicao_id INT REFERENCES instituicoes(id)
);

-- Inserir alguns usuários
INSERT INTO usuarios (nome, email, telefone, senha, tipo_usuario_id, documento_identificacao) VALUES
('João Silva', 'joao.silva@gmail.com', '(11) 98765-4321', 'senha123', 1, '223.456.789-00'),
('Maria Oliveira', 'maria.oliveira@gmail.com', '(11) 91234-5678', 'senha456', 2, '123.456.789-00'),
('Carlos Santos', 'admin@admin.com', '(11) 99876-5432', 'senha789', 3, '123.456.999-99');

-- Inserir alguns locais de retirada
INSERT INTO locais_retirada (nome, endereco, horario_disponivel) VALUES
('Centro de Distribuição A', 'Rua das Flores, 123', '08:00 - 18:00'),
('Centro de Distribuição B', 'Avenida Brasil, 456', '09:00 - 17:00');

-- Inserir algumas instituições
INSERT INTO instituicoes (nome, contato) VALUES
('Instituição de Apoio Social', 'contato@apoiosocial.org'),
('ONG Esperança', 'contato@ongesperanca.org');
```

## Tecnologias Utilizadas
- **Backend**: Entity Framework para gerenciamento do banco de dados.
- **Banco de Dados**: PostgreSQL, com suporte ao Entity Framework.
- **Linguagem**: C# (.NET) para as APIs e lógica de negócios.

## Exemplos de Dados no Banco de Dados
- **Tipos de Usuários**: Doador, Beneficiário, Admin.
- **Usuários**: João Silva (Doador), Maria Oliveira (Beneficiário), Carlos Santos (Admin).
- **Tipos de Doação**: Dinheiro, Alimentos.
- **Locais de Retirada**: Centro de Distribuição A (Rua das Flores, 123), Centro de Distribuição B (Avenida Brasil, 456).
- **Instituições Participantes**: Instituição de Apoio Social, ONG Esperança.

## Como Executar o Projeto
Para executar o projeto, é necessário:

1. Ter o PostgreSQL instalado e configurado com a base de dados fornecida.
2. Executar os scripts SQL para criação e população das tabelas.
3. Utilizar o Entity Framework para realizar o `Scaffold-DbContext` e gerar os modelos:

   ```bash
   Scaffold-DbContext "Host=localhost;Database=fomezero;Username=postgre;Password=2812" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models
   ```

Com isso, a plataforma estará pronta para receber doações e permitir a gestão das retiradas.

---
