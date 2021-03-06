import axios from 'axios';

export default {
  searchForMovie(title) {
    // Use fetch so we can easily redirect to login page if not logged in.
    return fetch(process.env.VUE_APP_API_URL + 'Movie/SearchForMovie?title=' + title, {
      credentials: "same-origin",
      redirect: 'error'
    });
  },
  getMovies() {
    // Use fetch so we can easily redirect to login page if not logged in.
    return fetch(process.env.VUE_APP_API_URL + 'Movie', {
      credentials: "same-origin",
      redirect: 'error'
    });
  },
  getMoviesWithVotes() {
    // Use fetch so we can easily redirect to login page if not logged in.
    return fetch(process.env.VUE_APP_API_URL + 'Movie/GetListWithVotes', {
      credentials: "same-origin",
      redirect: 'error'
    });
  },
  getMoviesWithVotesAndMovieInfo() {
    // Use fetch so we can easily redirect to login page if not logged in.
    return fetch(process.env.VUE_APP_API_URL + 'Movie/GetListWithVotesAndMovieInfo', {
      credentials: "same-origin",
      redirect: 'error'
    });
  },
  getMyUpVotes() {
    // Use fetch so we can easily redirect to login page if not logged in.
    return fetch(process.env.VUE_APP_API_URL + 'Movie/GetMyUpVotes', {
      credentials: "same-origin",
      redirect: 'error'
    });
  },
  addMovie(model) {
    return axios.post(process.env.VUE_APP_API_URL + 'Movie/Add', model);
  },
  upVote(movieId) {
    return axios.post(process.env.VUE_APP_API_URL + 'Movie/UpVote/' + movieId, {});
  },
  downVote(movieId) {
    return axios.post(process.env.VUE_APP_API_URL + 'Movie/DownVote/' + movieId, {});
  }
}