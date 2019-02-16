using Xunit;

namespace Configuration.Test
{
    public class SessionTest
    {
        private readonly Session _sessionImpl;
        private readonly ISession _session;

        public SessionTest()
        {
            _sessionImpl = new Session();
            _session = _sessionImpl;
        }

        [Fact]
        public void InitialState()
        {
            Assert.Equal(string.Empty, _session.Username);
            Assert.Empty(_session.Token);
        }

        [Fact]
        public void Username()
        {
            string username = "user";
            _sessionImpl.Username = username;
            Assert.Equal(username, _session.Username);
        }

        [Fact]
        public void Token()
        {
            string token = "abcdefghijklmnñopqrstuvwxyz";
            _sessionImpl.Token = token;
            Assert.Equal(token, _session.Token);
        }
    }
}
