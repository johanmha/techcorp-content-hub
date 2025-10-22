export interface Author {
  id: string;
  name: string;
  bio: string;
  profileImage: string;
  email: string;
  title: string;
  socialLinks: string[];
}

export interface Category {
  id: string;
  name: string;
  slug: string;
  description: string;
  color: string;
}

export interface BlogPost {
  id: string;
  title: string;
  slug: string;
  summary: string;
  content: string;
  author?: Author; // Optional, as it's nullable in C#
  categories: Category[];
  publishedDate: string; // DateTime in C# often maps to string in TS
  featuredImage: string;
  tags: string[];
  readingTime: number;
}
