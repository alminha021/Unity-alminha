# SimSaúde

![image](https://github.com/user-attachments/assets/b6fb1a0b-cbcd-44ad-8a93-21387d821841)

PPGIA disciplina ZZ913 Digital Games 2024.2

Alunos: Luiz Resende, Raul Costa e Eduardo Rebouças

Professora: Andreia Formico

## Visão Geral

Simulador de um posto de saúde baseado em turnos em que estudantes devem gerenciar recursos e ter suas decisões avaliadas

- Objetivo: Estimular estudantes de profissionais da saúde na gestão de recursos e na escolha apropriada de medicamentos em diferentes tipos de necessidades dos pacientes.
- Público-Alvo: Estudantes e profissionais da área da saúde
- Ambiente: Posto de saúde

## Narrativa

- História/Enredo: O jogador torna-se médico recém promovido de um posto de saúde e tem a missão de gerenciar os recursos disponíveis para atender a população de um bairro populoso da cidade de Fortaleza.
- Personagem principal: Pessoa gestora do posto de saúde.
- Personagens secundários: NPCs, pessoas doentes que precisam de tratamento.
- Mundo e Cenários: Posto de saúde, subdividido em salas especializadas, como área de atendimento (recepção), enfermaria, e escritório.

## Diagrama de Fluxo

![image](https://github.com/user-attachments/assets/b7660b3f-11d1-4d4e-95d6-573844f1b580)

1. Início do Turno
  Pacientes chegam ao hospital e entram na fila
2. Tomada de Decisões
  - O Jogador interage com os pacientes e escolhe um medicamento para cada tipo de doença
  - O jogador avalia a quantidade de medicamentos que tem em estoque (Máx. 5)
  - O jogador pode ir na farmácia adquirir kits de medicamentos
  - O jogador pode ir no escritório avaliar seu desempenho
3. Avaliação de Desempenho
  - Se a decisão na escolha do medicamento for boa, o paciente é curado e o jogador recebe reputação
  - Se a decisão na escolha do medicamento  for ruim, o paciente sai ainda doente e o jogador perde reputação
4. Fim do Turno
  - Relatório de Desempenho
  - Preparação para o Próximo Turno

## Gameplay

- Descritivo: O sistema do jogo se baseia na reputação do jogador, que pode aumentar ou diminuir a cada turno dependendo do resultado dos tratamentos escolhidos. A reputação pode variar entre 0 e 100, começando com 30. Cada decisão de tratamento dos pacientes acertada, cura o paciente e faz o jogador ganhar reputação. Cada decisão errada, não trata o paciente e faz o jogador perder reputação. O jogo inicia com a formação da fila, que gera pacientes com doenças e forma uma fila na porta do hospital. O turno termina quando toda a fila é tratada. No turno 1, a fila deve ter 10 pacientes. No turno 2, 15 pacientes. No turno 3, 20 pacientes. O jogador pode carregar 5 medicamentos de cada tipo por vez. Ao acabar um tipo, ele deve buscar mais na farmácia. Na farmácia, um kit com 5 medicamentos custa 1 reputação. Então o jogador vai até a farmácia, abre o menu de medicamentos, escolhe o medicamento que quer comprar e confirma a transação pagando 1 reputação, onde seu estoque daquele medicamento é atualizado com 5 novas unidades. No início do jogo, o jogador tem 3 medicamentos de todos os tipos. O jogo acaba quando a reputação do jogador chega a zero e ele perde ou quando chega a 100 e ele ganha.
- Mecânica: o jogador controla os movimentos de seu player e a interação com o jogo usando o mouse
- Dinâmica: O gestor fica na sala dele e toma todas as decisões. No processamento das decisões tomadas, os NPCs (Non Playable Characters) dos pacientes se locomovem automaticamente dentro do ambiente do posto de saúde. O gestor deve adquirir e disponibilizar medicamentos. Se acabarem os medicamentos e as pessoas não forem atendidas, o gestor recebe uma advertência.
- Objetivos do Jogador: Manter o posto funcionando de forma otimizada e adquirir o máximo possível de reputação
- Regras: Uso de reputação para adquirir medicamentos e curar pessoas. O jogador perde o jogo se a reputação chegar a zero e ganha se a reputação chegar a 100. Se o jogador curar todos os pacientes de um determinado turno, ele ganha 10 de reputação ao fim do turno.
- Progressão: Cada turno no jogo será a interação com a quantidade disponível de pacientes doentes naquele turno. O primeiro turno são 10 pacientes doentes, o segundo são 15 pacientes doentes e o terceiro turno são 20 pacientes doentes.
- Nível de Dificuldade: Aumento da quantidade de pacientes a serem tratados.
- Sistema de pontuação: Sistema é modificado ao vivo e baseado na reputação. O jogador começa com 30 de reputação. Para cada tratamento de sucesso, recebe 1 ponto de reputação e para cada tratamento indevido, perde 1 reputação.

## Interface de Usuário (UI/UX)

- HUD: Reputação, quantidade de medicamentos disponíveis, pacientes na fila.
- Menus: Menus de medicamentos na farmácia e menu com relatório de desempenho no escritório.
- Controles: Mouse - Point and click.
