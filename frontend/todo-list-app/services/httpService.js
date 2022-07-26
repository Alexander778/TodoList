import axios from 'axios';

const HTTP = axios.create({
  withCredentials: false,
  baseURL: 'http://localhost:16894/' 
});

const get = (route, params) => HTTP.get(route, { params }).then((res) => res.data);

const post = (route, params, headers) => {
  if (headers != null) {
    return HTTP.post(route, params, headers).then((res) => res.data).catch((err) => err.response);
  }
  return HTTP.post(route, params).then((res) => res.data).catch((err) => err.response);
};

export default {
  get,
  post
};