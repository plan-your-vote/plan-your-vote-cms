export class Image {
  description: string;
  format: string;
  id: string;
  themeName: string;
  type: string;
  value: string;

  constructor(
    description: string,
    format: string,
    id: string,
    themeName: string,
    type: string,
    value: string
  ) {
    this.description = description;
    this.format = format;
    this.id = id;
    this.themeName = themeName;
    this.type = type;
    this.value = value;
  }
}
