export const  saveTokenToLocalStorage = (token: string) => {
  localStorage.setItem('authToken', token);
}