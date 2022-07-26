import httpService from './httpService';

const getUsers = () => httpService.get('/todoList/users');

// const remove = (id, projectId, interviewId) => httpService.post(`/api/file/remove/${id}/${projectId}/${interviewId}`);

// const update = (id, object, projectId) => httpService.post(`/api/file/update/${id}/${projectId}`, object);

export default {
  getUsers
};