# TicTacToe
Для разработки сайта и мобильного приложения для игры в крестики нолики 3x3 для двух игроков требуется реализовать web api. Игра проходит по обычным правилам.

Для работы потребуется сменить ссылку на MSSql бд в appsettings.json connectionStrings/default

## API поддерживает
* [GET] Players : выводит всех игроков
* [GET] Players?id : выводит игрока по id
* [POST] Players : принимает из тела обьект Player и создает игрока
* [PUT] Players : принимает из тела обьект Player и изменяет его
* [DELETE] Players?id : удаляет игрока по id
###
* [GET] Rooms : выводит все лобби
* [GET] Rooms?id : выводит лобби по id
* [POST] Rooms : создает лобби принимая из тела 
  + int playerXId 
  + int player0Id
* [POST] Rooms/roomId/steps : создает шаг для лобби roomId принимая аргументы
  + int row
  + int column
* [DELETE] Rooms/roomId/steps : удаляет последний шаг для лобби roomId
* [DELETE] Rooms/roomId : удаляет лобби по roomId
