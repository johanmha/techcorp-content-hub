import axios from 'axios';
import { BlogPost, Author, Category } from '../types';

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL || 'http://localhost:5000/api'; // Adjust as per your backend API URL

const api = axios.create({
  baseURL: API_BASE_URL,
});

export const getBlogPosts = async (): Promise<BlogPost[]> => {
  const response = await api.get<BlogPost[]>('/content/blogposts');
  return response.data;
};

export const getBlogPostBySlug = async (slug: string): Promise<BlogPost> => {
  const response = await api.get<BlogPost>(`/content/blogposts/${slug}`);
  return response.data;
};

export const getAuthors = async (): Promise<Author[]> => {
  const response = await api.get<Author[]>('/content/authors');
  return response.data;
};

export const getAuthorById = async (id: string): Promise<Author> => {
  const response = await api.get<Author>(`/content/authors/${id}`);
  return response.data;
};

export const getCategories = async (): Promise<Category[]> => {
  const response = await api.get<Category[]>('/content/categories');
  return response.data;
};

export const getCategoryBySlug = async (slug: string): Promise<Category> => {
  const response = await api.get<Category>(`/content/categories/${slug}`);
  return response.data;
};
