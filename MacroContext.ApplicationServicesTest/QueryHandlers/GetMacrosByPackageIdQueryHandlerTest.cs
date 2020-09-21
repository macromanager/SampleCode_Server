using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacroContext.Contract.Dto;
using MacroContext.Persistance;
using NSubstitute;
using System.Collections.Generic;
using MacroContext.Domain;
using MacroContext.ApplicationServices.QueryHandlers;
using MacroContext.Shared.Utilities;
using MacroContext.Contract.Queries;

namespace ApplicationServices.Test.QueryHandlers
{
    [TestClass]
    public class GetMacrosByPackageIdQueryHandlerTest
    {
        private GetMacrosByPackageIdQueryHandler _handler;
        private IUnitOfWork _mockUow;
        private IMyMapper _mockMapper;
        private IMacroProfileRepository _mockRepo;
        private GetMacrosByPackageIdQuery _query;
        private MacroProfileDto _profileDto;
        private MacroProfile[] _profiles = new MacroProfile[1] { new MacroProfile(Guid.NewGuid(), Guid.NewGuid(), new Macro(Guid.NewGuid()))};

        [TestInitialize]
        public void TestInitialize()
        {
            _query = new GetMacrosByPackageIdQuery(Guid.NewGuid());

            _mockRepo = Substitute.For<IMacroProfileRepository>();
            _mockRepo.GetByPackageId(Arg.Any<Guid>()).Returns(_profiles);
            _mockUow = Substitute.For<IUnitOfWork>();
            _mockUow.MacroProfiles.Returns(_mockRepo);
            _profileDto = new MacroProfileDto();

            _mockMapper = Substitute.For<IMyMapper>();
            _mockMapper.Map<MacroProfile, MacroProfileDto>(Arg.Any<MacroProfile>())
                .Returns(new MacroProfileDto(_profiles[0].Id, Guid.Empty, Guid.Empty));
            _mockMapper.Map<Macro, MacroDto>(Arg.Any<Macro>())
               .Returns(new MacroDto(_profiles[0].Macro.Id));

            _handler = new GetMacrosByPackageIdQueryHandler(_mockUow, _mockMapper);

        }

        [TestMethod]
        public void HandleQuery_RepoQueriesDataWithCorrectId()
        {
            _handler.Handle(_query);
            _mockRepo.Received().GetByPackageId(Arg.Is(_query.PackageId));
        }

        [TestMethod]
        public void HandleQuery_CallsMapperForProfile()
        {
            var data = _handler.Handle(_query);
            _mockMapper.Received().Map<MacroProfile, MacroProfileDto>(Arg.Any<MacroProfile>());

        }

        [TestMethod]
        public void HandleQuery_CallsMapperForMacro()
        {
            var data = _handler.Handle(_query);
            _mockMapper.Received().Map<Macro, MacroDto>(Arg.Any<Macro>());

        }

        //[TestMethod]
        //public void GetCompleteMacros_ResultIsAccurate()
        //{
        //    var isCorrect = true;
        //    var data = _handler.GetCompleteMacros(_profiles).ToArray();
        //    if(data.Count() != _profiles.Count()) { isCorrect = false; }
        //    if(data[0].MacroProfile.Id != _profiles[0].Id) { isCorrect = false; }
        //    if(data[0].Macro.Id != _profiles[0].Id) { isCorrect = false; }
        //    Assert.IsTrue(isCorrect == true);

        //}



        //[TestMethod]
        //public void HandleQuery_CallsGetCompleteMacros()
        //{
        //    var handler = NSubstitute.Substitute.For<GetMacrosByPackageIdQueryHandler>(_mockUow, _mockMapper);
        //    var data = handler.Handle(_query);
        //    handler.Received().GetCompleteMacros(Arg.Any<IEnumerable<MacroProfile>>()); // this calls method again and causes error

        //}

    }
}
