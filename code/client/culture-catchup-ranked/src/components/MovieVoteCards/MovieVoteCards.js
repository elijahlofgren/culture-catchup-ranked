import './MovieVoteCards.scss';
import MovieService from "../../services/MovieService.js";

export default {
  props: {
    msg: String
  },
  data: function () {
    return {
     loading: true,
     movies: []
    };
  },
  created() {
    let vm = this;
    vm.getMovies();
  },
  methods: {
    hasVoted(item) {
      if (item.justVoted) {
        return true;
      } else {
        return false;
      }
    },
    getMovies() {
      let vm = this;
     
      MovieService.getMoviesWithVotesAndMovieInfo()
        .then((response) => {
          vm.loading = false;
          console.log("getMoviesWithVotesAndMovieInfo response = ");
          console.log(JSON.stringify(response));
          console.log(response);
          response.json().then((data) => {
            let movies = data;
            vm.movies = movies;
          });

        })
        .catch(error => {
          console.error('Error =');
          console.error(error);
          window.location.href = '/Identity/Account/Login';

        });
    },
    upVote(item) {
      Vue.set(item, 'justVoted', true);
      let movieItem = item.movie;
      MovieService.upVote(movieItem.id)
        .then(() => {
          console.log("Up Voted");
        })
        .catch(error => {
          console.error(error);
        });
    },
    downVote(item) {
      Vue.set(item, 'justVoted', true);
      let movieItem = item.movie;
      MovieService.downVote(movieItem.id)
        .then(() => {
          console.log("Down Voted");
        })
        .catch(error => {
          console.error(error);
        });
    },
  }
};