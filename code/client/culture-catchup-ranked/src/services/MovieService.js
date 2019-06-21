import axios from 'axios';

export default {
  getMovies() {
    // Use fetch so we can easily redirect to login page if not logged in.
    return fetch(process.env.VUE_APP_API_URL + 'Movie', {
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