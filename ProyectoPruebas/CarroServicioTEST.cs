using AutoMapper;
using Moq;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Carro;
using WebAvanzadaIICuatrimestre.DAL.Entidades;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

namespace ProyectoPruebas;

public class CarroServicioTEST
{
    [Fact]
    public async Task CreateCarro_ShouldReturnSuccess_WhenDataIsValid()
    {
        var repo = new Mock<IRepositorioGenerico<Carro>>();
        var mapper = new Mock<IMapper>();
        var servicio = new CarroServicio(repo.Object, mapper.Object);

        var dto = new CarroDto
        {
            Marca = "Ferrari",
            Chocado = 0,
            ValorFiscal = 150000
        };

        var entity = new Carro
        {
            Marca = "Ferrari",
            Chocado = 0,
            ValorFiscal = 150000
        };

        mapper.Setup(m => m.Map<Carro>(It.IsAny<CarroDto>())).Returns(entity);
        repo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);
        repo.Setup(r => r.AgregarAsync(It.IsAny<Carro>()));

        var result = await servicio.CreateCarro(dto);

        Assert.True(result.esCorrecto);
        Assert.Equal(200, result.codigo);
        repo.Verify(r => r.AgregarAsync(It.IsAny<Carro>()), Times.Once);
        repo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
