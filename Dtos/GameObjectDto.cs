namespace Rokuro.Dtos;

public record GameObjectDto(
	string Name,
	string Sprite,
	string Class,
	int X,
	int Y,
	List<CustomPropertyDto> CustomProperties
);
