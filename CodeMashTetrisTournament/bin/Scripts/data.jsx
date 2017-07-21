
var HighScore = React.createClass({
    rawMarkup: function() {
        var md = new Remarkable();
        var rawMarkup = md.render(this.props.children.toString());
        return { __html: rawMarkup };
    },

    render: function() {
        return (
            <div className="row score">
                <div className="col-md-1">
                    <h2>{this.props.rank}</h2>
                </div>
                <div className="col-md-8">
                    <h2>{this.props.name}</h2>
                </div>
                <div className="col-md-3">
                    <h2>{this.props.score}</h2>
                </div>
            </div>
      );
    }
});

var HighScoreList = React.createClass({
  render: function() {
      var highscoreNodes = this.props.data.map(function (highscore) {
      return (
        <HighScore name={highscore.Name} key={highscore.Id} score={highscore.Score} rank={highscore.Rank}>
        </HighScore>
      );
    });
    return (
      <div className="highscoreList">{highscoreNodes}
      </div>
    );
  }
});


var HighScores = React.createClass({
    loadHighScoresFromServer: function () {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        }.bind(this);
        xhr.send();
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentDidMount: function () {
        this.loadHighScoresFromServer();
        window.setInterval(this.loadHighScoresFromServer, this.props.pollInterval);
    },

  render: function() {
    return (
      <div className="highscores">
        <h1>High Scores</h1>
        <HighScoreList data={this.state.data} />
      </div>
    );
  }
});

ReactDOM.render(
  <HighScores url="/CodeMashTetrisTournament/HighScores" pollInterval={10000} />,
  document.getElementById('content')
);