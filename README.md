# PDFService 💾

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![.NET Version](https://img.shields.io/badge/.NET-7-blue)
![Docker](https://img.shields.io/badge/docker-supported-blue)

¡Buenas! Este proyecto es un microservicio que realicé en mi trabajo. Está hecho en NET 7.0 y tiene como objetivo realizar diferentes acciones/manejos de archivos PDF.

### 📦 Prerrequisitos
- ![.NET](https://img.shields.io/badge/.NET-7-blue)
- ![Docker](https://img.shields.io/badge/docker-supported-blue)

## 🚀 Cómo Empezar

Para arrancar el proyecto:
1. **Clona el repositorio**  
   Usa el siguiente comando para clonar el repositorio desde la rama master:
   ```bash
   git clone https://github.com/juanlotito/PDFService.git  
2. Desde la consola, dirigite a la carpeta en donde se clonó el proyecto:
    ```bash
    cd PDFService
3. Para ejecutar la solución, realizá un docker compose (no te olvides del --build!)
    ```bash
    docker-compose up -d --build
4. Si el proyecto corrió correctamente, deberías ver un elemento si ejecutas el comando *docker ps*
    - PDFService

## 🛠 Uso

El swagger (o documentación) lo encontrás en /swagger! Igual también podes pegarle desde Postman, API Dog o la herramienta que quieras.

Como mencioné anteriormente, el microservicio tiene como objetivo hacer manejos con archivos PDFs, por ahora cuenta con los siguientes métodos:

### Método `/api/pdf/combine`
- **Objetivo**: Recibir dos o más PDFs y devolver un único PDF con todos los recibidos compaginados. El request body es multipart/form-data

### Método `/api/pdf/html`
- **Objetivo**: Recibir por body un HTML como string y convertirlo a un PDF respetando imágenes y estilos. También recibe en el body una propiedad que indica si se quiere como devolución un array de bits o la descarga del archivo. El request body es application/json.

## 📬 Contacto

Con eso culminaríamos, muchas gracias por leer! Te dejo mis redes:

- **Email**: [juanilotito@gmail.com](mailto:juanilotito@gmail.com)
- **GitHub**: [juanlotito](https://github.com/juanlotito)
- **LinkedIn**: [Juan Ignacio Lotito](https://www.linkedin.com/in/juan-ignacio-lotito-601157195/)

Gracias! :)







