namespace Rokuro.Dtos;

public record GameObjectDto(
	string Name,
	string Sprite,
	string Class,
	string Camera,
	int X,
	int Y,
	List<CustomPropertyDto> CustomProperties
);
