import './MyUpVotes.scss';
import MovieService from "../../services/MovieService.js";

export default {
  props: {
    msg: String
  },
  data: function () {
    return {
     movies: []
    };
  },
  created() {
    let vm = this;
    vm.fetchMyUpvotes();
  },
  methods: {
    fetchMyUpvotes() {
      let vm = this;
     
      MovieService.getMyUpVotes()
        .then((response) => {
          console.log("getMyUpVotes response = ");
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
    upVote(movieItem) {
      MovieService.upVote(movieItem.id)
        .then(() => {
          console.log("Up Voted");
        })
        .catch(error => {
          console.error(error);
        });
    },
    downVote(movieItem) {
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