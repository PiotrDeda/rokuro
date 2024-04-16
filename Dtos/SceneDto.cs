namespace Rokuro.Dtos;

public record SceneDto(
	string Name,
	string Class,
	List<GameObjectDto> GameObjects,
	List<CameraDto> Cameras,
	List<CustomPropertyDto> CustomProperties
);
