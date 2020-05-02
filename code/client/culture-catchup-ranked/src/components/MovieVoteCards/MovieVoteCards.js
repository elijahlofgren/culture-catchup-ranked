import './MovieVoteCards.scss';
import Vue from 'vue';
import MovieService from "../../services/MovieService.js";
import AddMovie from '../AddMovie/AddMovie.vue';

export default {
  components: {
    AddMovie
  },
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
      } else if (item.currentUserUpVoted || item.currentUserDownVoted) {
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
      Vue.set(item, 'voteForSubmitting', true);
      let movieItem = item.movie;
      MovieService.upVote(movieItem.id)
        .then(() => {
          Vue.set(item, 'voteForSubmitting', false);
          console.log("Up Voted");
          Vue.set(item, 'currentUserUpVoted', true);
          item.upVoteCount++;
        })
        .catch(error => {
          console.error(error);
        });
    },
    downVote(item) {
      Vue.set(item, 'justVoted', true);
      Vue.set(item, 'voteAgainstSubmitting', false);
      let movieItem = item.movie;
      MovieService.downVote(movieItem.id)
        .then(() => {
          console.log("Down Voted");
          item.downVoteCount++;
          Vue.set(item, 'voteAgainstSubmitting', false);
          Vue.set(item, 'currentUserDownVoted', true);
        })
        .catch(error => {
          console.error(error);
        });
    },
  }
};