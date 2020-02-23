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
    }
  }
};