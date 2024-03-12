namespace Rokuro.Dtos;

public record GameObjectDto(
	string Name,
	string Class,
	string SpriteType,
	string Sprite,
	string Camera,
	int X,
	int Y,
	List<CustomPropertyDto> CustomProperties
);
