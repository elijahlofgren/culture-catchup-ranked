import axios from 'axios';

export default {
  getMovies() {
    // todo put URL in config file.
    return axios('https://localhost:5001/api/Movie');
  }
}