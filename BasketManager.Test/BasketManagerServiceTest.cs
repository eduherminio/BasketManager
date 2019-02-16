using BasketManager.Dao;
using BasketManager.Model;
using BasketManager.Service;
using BasketManager.Service.Impl;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BasketManager.Test
{
    public class BasketManagerServiceTest
    {
        private readonly IBasketManagerService _basketManagerService;
        private readonly Mock<IItemDao> _daoMock;

        public BasketManagerServiceTest()
        {
            _daoMock = new Mock<IItemDao>();
            _daoMock
                .Setup(dao => dao.Create(It.IsAny<Foo>()))
                .Returns(new Foo() { Id = "mockId1" });
            _daoMock
                .Setup(dao => dao.Create(It.IsAny<Bar>()))
                .Returns(new Bar() { Id = "mockId2" });
            _daoMock
                .Setup(dao => dao.Load())
                .Returns(new List<Item>() { new Foo() { Id = "mockId3" } });
            _daoMock
                .Setup(dao => dao.Remove(It.IsAny<string>()));

            _basketManagerService = new BasketManagerService(_daoMock.Object);
        }

        [Fact]
        public void Add()
        {
            _basketManagerService.Add(new Foo());
            _daoMock.Verify(mock => mock.Create(It.IsAny<Foo>()), Times.Once);

            _basketManagerService.Add(new Bar());
            _daoMock.Verify(mock => mock.Create(It.IsAny<Bar>()), Times.Once);
        }

        [Fact]
        public void Load()
        {
            _basketManagerService.Load();
            _daoMock.Verify(mock => mock.Load(), Times.Once);
        }

        [Fact]
        public void Clear()
        {
            _basketManagerService.Clear();
            _daoMock.Verify(mock => mock.Clear(), Times.Once);
        }

        [Fact]
        public void Remove()
        {
            _basketManagerService.Remove(Guid.NewGuid().ToString());
            _daoMock.Verify(mock => mock.Remove(It.IsAny<string>()), Times.Once);
        }
    }
}
