namespace Rokuro.Dtos;

public record SceneDto(string Name, List<GameObjectDto> GameObjects, List<CameraDto> Cameras);
