import httpService from './httpService';

const getUsers = () => httpService.get('/todoList/users');
const addUser = (userInput) => httpService.post('/todoList/add-user', userInput);
const updateUser = (userInput) => httpService.post('/todoList/update-user', userInput);

const getCategories = (userId, categoryId = null, todoTaskStatusId = null) => {
  let queryString = `/todoList/categories?userId=${userId}`;

  if (categoryId != null) {
    queryString += `&categoryId=${categoryId}`;
  }

  if (todoTaskStatusId != null) {
    queryString += `&todoTaskStatusId=${todoTaskStatusId}`;
  }

  return httpService.get(queryString);
}
const addCategory = (categoryInput) => httpService.post('/todoList/add-category', categoryInput);
const updateCategory = (categoryInput) => httpService.post('/todoList/update-category', categoryInput);
const removeCategory = (id) => httpService.post(`/todoList/remove-category/${id}`);

const addTask = (taskInput) => httpService.post('/todoList/add-task', taskInput);
const updateTask = (taskInput) => httpService.post('/todoList/update-task', taskInput);
const removeTask = (id) => httpService.post(`/todoList/remove-task/${id}`);

export default {
  getUsers,
  addUser,
  updateUser,
  getCategories,
  addCategory,
  updateCategory,
  removeCategory,
  addTask,
  updateTask,
  removeTask
};