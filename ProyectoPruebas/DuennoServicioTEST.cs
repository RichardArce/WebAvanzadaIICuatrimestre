using System;
using System.Linq.Expressions;
using AutoMapper;
using Moq;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Duenno;
using WebAvanzadaIICuatrimestre.DAL.Entidades;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

namespace ProyectoPruebas;

public class DuennoServicioTEST
{
    [Fact]
    public async Task CreateDuenno_ShouldReturnError_WhenDuennoIsNull()
    {
        var repo = new Mock<IRepositorioGenerico<Duenno>>();
        var mapper = new Mock<IMapper>();
        var servicio = new DuennoServicio(mapper.Object, repo.Object);

        var result = await servicio.CreateDuenno(null);

        Assert.False(result.esCorrecto);
        Assert.Equal(400, result.codigo);
        Assert.Equal("Duenno inválido", result.mensaje);
        repo.Verify(r => r.AgregarAsync(It.IsAny<Duenno>()), Times.Never);
    }

    [Fact]
    public async Task CreateDuenno_ShouldReturnSuccess_WhenDuennoIsValid()
    {
        var repo = new Mock<IRepositorioGenerico<Duenno>>();
        var mapper = new Mock<IMapper>();
        var servicio = new DuennoServicio(mapper.Object, repo.Object);

        var dto = new DuennoDto { Nombre = "Juan", Apellido1 = "Perez", Apellido2 = "Lopez", Edad = 25 };
        var entity = new Duenno { Nombre = "Juan", Edad = 25 };

        mapper.Setup(m => m.Map<Duenno>(It.IsAny<DuennoDto>())).Returns(entity);
        repo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        var result = await servicio.CreateDuenno(dto);

        Assert.True(result.esCorrecto);
        Assert.Equal(200, result.codigo);
        repo.Verify(r => r.AgregarAsync(It.IsAny<Duenno>()), Times.Once);
        repo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetDuennoById_ShouldReturnError_WhenNotFound()
    {
        var repo = new Mock<IRepositorioGenerico<Duenno>>();
        var mapper = new Mock<IMapper>();
        var servicio = new DuennoServicio(mapper.Object, repo.Object);

        repo.Setup(r => r.ObtenerPorIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<Expression<Func<Duenno, object>>[]>() ))
            .ReturnsAsync((Duenno)null);

        var result = await servicio.GetDuennoById(999);

        Assert.False(result.esCorrecto);
        Assert.Equal(404, result.codigo);
        Assert.Equal("Dueño no encontrado", result.mensaje);
    }

    [Fact]
    public async Task UpdateDuenno_ShouldReturnSuccess_WhenDuennoIsValid()
    {
        var repo = new Mock<IRepositorioGenerico<Duenno>>();
        var mapper = new Mock<IMapper>();
        var servicio = new DuennoServicio(mapper.Object, repo.Object);

        var dto = new DuennoDto { Id = 1, Nombre = "Juan", Apellido1 = "Perez", Apellido2 = "Lopez", Edad = 25 };
        var entity = new Duenno { Id = 1, Nombre = "Juan", Edad = 25 };

        mapper.Setup(m => m.Map<Duenno>(It.IsAny<DuennoDto>())).Returns(entity);
        repo.Setup(r => r.BuscarAsync(It.IsAny<Expression<Func<Duenno, bool>>>(), It.IsAny<bool>(), It.IsAny<Expression<Func<Duenno, object>>[]>() ))
            .ReturnsAsync(new Duenno { Id = 1 });
        repo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        var result = await servicio.UpdateDuenno(dto);

        Assert.True(result.esCorrecto);
        Assert.Equal(200, result.codigo);
        repo.Verify(r => r.ActualizarAsync(It.IsAny<Duenno>()), Times.Once);
    }
}

