import axios from 'axios';
import { useQuery } from '@tanstack/react-query';
import { BlogPost, Author, Category } from '../types';

// Use proxy in development (/api -> http://localhost:5121/api)
// In production, set REACT_APP_API_BASE_URL to your actual API URL
const API_BASE_URL = process.env.REACT_APP_API_BASE_URL || '/api';

const api = axios.create({
  baseURL: API_BASE_URL,
});

// API functions
export const getBlogPosts = async (): Promise<BlogPost[]> => {
  const response = await api.get<BlogPost[]>('/content/posts');
  return response.data;
};

export const getBlogPostBySlug = async (slug: string): Promise<BlogPost> => {
  const response = await api.get<BlogPost>(`/content/posts/${slug}`);
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

// React Query hooks
export const useBlogPosts = () => {
  return useQuery({
    queryKey: ['blogPosts'],
    queryFn: getBlogPosts,
  });
};

export const useBlogPost = (slug: string) => {
  return useQuery({
    queryKey: ['blogPost', slug],
    queryFn: () => getBlogPostBySlug(slug),
    enabled: !!slug,
  });
};

export const useAuthors = () => {
  return useQuery({
    queryKey: ['authors'],
    queryFn: getAuthors,
  });
};

export const useAuthor = (id: string) => {
  return useQuery({
    queryKey: ['author', id],
    queryFn: () => getAuthorById(id),
    enabled: !!id,
  });
};

export const useCategories = () => {
  return useQuery({
    queryKey: ['categories'],
    queryFn: getCategories,
  });
};

export const useCategory = (slug: string) => {
  return useQuery({
    queryKey: ['category', slug],
    queryFn: () => getCategoryBySlug(slug),
    enabled: !!slug,
  });
};
