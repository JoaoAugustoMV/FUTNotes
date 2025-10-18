# FutNotes

## 🇺🇸 English Version

### 📝 Description

**FutNotes** is an application designed for sports commentators and journalists, allowing them to record real-time notes, match evaluations, and player performance ratings while watching football matches.  

The project was created primarily as a **practical study** of **CQRS**, **Event Sourcing**, **Saga Pattern**, and **Kafka**, using **.NET 8** as the main backend framework.

---

### 🎯 Project Goal

To build a **modular and scalable application**, evolving from a simple MVP into a complete **match analysis platform**.

---

### 🧠 Technologies Used

#### 🖥️ Backend (.NET 8 / C#)
- **ASP.NET Core Web API**  
- **Entity Framework Core**  
- **PostgreSQL**  
- **CQRS + Mediator Pattern (MediatR)**  
- **Kurrent.IO** – Event Sourcing database  
- **Kafka** (Messaging / Event Streaming)  
- **JWT Authentication**
- **gRPC** for internal communication between contexts/microservices  

#### 📱 Frontend (Planned)
- **Flutter**

---

### 🚀 Roadmap (Planned Evolution)

- [x] User registration and authentication  
- [x] CRUD for matches
- [ ] Notes creation for matches
- [ ] Player and match evaluation features  
- [ ] Kafka integration for domain events  
- [ ] Event Sourcing implementation  
- [ ] Optimized read models (Full CQRS)  
- [ ] Flutter interface  

---


## 🇧🇷 Versão em Português

### 📝 Descrição

O **FutNotes** é um aplicativo voltado para comentaristas e jornalistas esportivos, que permite registrar anotações, avaliações de partidas e desempenho de jogadores em tempo real enquanto assistem aos jogos.  

O projeto nasceu com o objetivo principal de **servir como estudo prático** de **CQRS, Event Sourcing, Saga Pattern e Kafka** utilizando **.NET 8**.

---

### 🎯 Objetivo do Projeto

Criar uma aplicação modular e escalável, evoluindo de um MVP para um sistema completo de análise de partidas.

---

### Tecnologias Utilizadas

#### Backend (.NET 8 / C#)
- ASP.NET Core Web API  
- Entity Framework Core  
- PostgreSQL  
- CQRS + Mediator Pattern(Mediatr)  
- Kurrent.IO - Banco de dados para o Event Sourcing  
- Kafka (mensageria/eventos)  
- JWT para autenticação  
- **gRPC** para comunicação interna entre contextos/microsserviços

#### Frontend (planejado)
- Flutter

---


### 🚀 Roadmap (Evolução Planejada)

- [x] Cadastro e login de usuário  
- [x] CRUD de partidas
- [ ] Criação de anotações para partidas e times
- [ ] Criação de notas de jogadores  
- [ ] Integração com Kafka para eventos de domínio  
- [ ] Implementação de Event Sourcing  
- [ ] Read Models otimizados (CQRS completo)  
- [ ] Interface Flutter  

---