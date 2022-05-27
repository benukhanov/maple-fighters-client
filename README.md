<h1 align="center">Maple Fighters</h1>
<p align="center">
  <img src="docs/images/maplestory-icon.png" width="100px" height="100px"/>
  <br><i>A small online game similar to MapleStory</i><br>
</p>
<p align="center">
  <a href="http://maplefighters.io"><strong>maplefighters.io</strong></a>
  <br>
</p>

## About

Maple Fighters is a multiplayer online game inspired by MapleStory. Players can choose a fighter to travel the world and fight monsters with other fighters.

Please **★ Star** this repository if you like it and find it useful. Made With :heart: For Open Source Community!

## Play Online

Maple Fighters is available at [maplefighters.io](http://maplefighters.io). This is a web game, no installation required. Supported in any web browser with internet connection. Small, optimized, and incredibly fast! 🚀

## Screenshots

| Lobby                             | The Dark Forest                             |
| --------------------------------- | ------------------------------------------- |
| <img src="docs/images/lobby.png"> | <img src="docs/images/the-dark-forest.png"> |

## Technology

**Game Engine**: Unity WebGL  
**Client**: C#, React.js (_C# is compiled to C++ and finally to WebAssembly_)  
**Server**: C#, Rust, Node.js  
**Database**: MongoDB, PostgreSQL  
**Reverse Proxy**: Nginx

## Projects

| Name                                               | Language | Description                                    |
| -------------------------------------------------- | -------- | ---------------------------------------------- |
| [maple-fighters](./src/maple-fighters)             | C#       | Game code of the Maple Fighters.               |
| [frontend](./src/frontend)                         | React.js | Provides game files generated by Unity.        |
| [auth-service](./src/auth-service)                 | C#       | Stores user data and verifies user.            |
| [game-service](./src/game-service)                 | C#       | Creates game scenes, players and game objects. |
| [gameprovider-service](./src/gameprovider-service) | Rust     | Provides a list of game servers.               |
| [character-service](./src/character-service)       | Rust     | Stores the player's character data.            |
| [chat-service](./src/chat-service)                 | Node.js  | Allows players to communicate with each other. |

## Build Status

| Name                 | Status                                                                                                                                                                                                                                |
| -------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Maple Fighters       | [![Unity Build](https://github.com/codingben/maple-fighters/actions/workflows/unity-build.yml/badge.svg)](https://github.com/codingben/maple-fighters/actions/workflows/unity-build.yml)                                              |
| Frontend             | [![Frontend Build](https://github.com/codingben/maple-fighters/actions/workflows/frontend-build.yml/badge.svg)](https://github.com/codingben/maple-fighters/actions/workflows/frontend-build.yml)                                     |
| Auth Service         | [![Auth Service Build](https://github.com/codingben/maple-fighters/actions/workflows/auth-service-build.yml/badge.svg)](https://github.com/codingben/maple-fighters/actions/workflows/auth-service-build.yml)                         |
| Game Service         | [![Game Service Build](https://github.com/codingben/maple-fighters/actions/workflows/game-service-build.yml/badge.svg)](https://github.com/codingben/maple-fighters/actions/workflows/game-service-build.yml)                         |
| GameProvider Service | [![GameProvider Service Build](https://github.com/codingben/maple-fighters/actions/workflows/gameprovider-service-build.yml/badge.svg)](https://github.com/codingben/maple-fighters/actions/workflows/gameprovider-service-build.yml) |
| Character Service    | [![Character Service Build](https://github.com/codingben/maple-fighters/actions/workflows/character-service-build.yml/badge.svg)](https://github.com/codingben/maple-fighters/actions/workflows/character-service-build.yml)          |
| Chat Service         | [![Chat Service Build](https://github.com/codingben/maple-fighters/actions/workflows/chat-service-build.yml/badge.svg)](https://github.com/codingben/maple-fighters/actions/workflows/chat-service-build.yml)                         |

## Contributing

Please read the [contributing guidelines](CONTRIBUTING.md).

## Artwork

The artwork is owned by Nexon Co., Ltd and will never be used commercially.

## License

[AGPL](https://choosealicense.com/licenses/agpl-3.0/)
