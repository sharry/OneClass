// this page is for teacher to create a class, it has an image in the top, with a botton on top of it, placed in the bottom right corner, then a classroom name input, a class code input, a subject a discription ,and a create button

import React from 'react';
import { useState } from 'react';
import { useHistory } from 'react-router-dom';
import { useMutation } from '@tanstack/react-query';
import { createClass } from '../api/class';
import { useOneClassContext } from '../context/OneClassContext';
import { useQueryClient } from '@tanstack/react-query';

const CreateClass = () => {
    const [name, setName] = useState('');
    const [code, setCode] = useState('');
    const [subject, setSubject] = useState('');
    const [description, setDescription] = useState('');
    const history = useHistory();
    const queryClient = useQueryClient();
    const { user } = useOneClassContext();
    const { mutate } = useMutation(createClass, {
        onSuccess: () => {
            queryClient.invalidateQueries('classes');
            history.push('/classes');
        },
    });

    const handleSubmit = (e) => {
        e.preventDefault();
        mutate({ name, code, subject, description, teacher: user._id });
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen py-2 -mt-28 sm:px-6 lg:px-8">
            <div className="w-full max-w-md p-6 my-8 overflow-hidden bg-white rounded-lg shadow-md dark:bg-gray-800">
                <div className="flex items-center justify-center">
                    <span className="text-xl font-semibold text-gray-800 dark:text-white">Create Class</span>
                </div>

                <div className="mt-4">
                    <form onSubmit={handleSubmit}>
                        <label className="block text-sm">
                            <span className="text-gray-700 dark:text-gray-400">Class Name</span>
                            <input
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                                className="block w-full px-4 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md dark:bg-gray-700 dark:text-gray-300 dark:border-gray-600 dark:focus:shadow-outline-gray focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
                                placeholder="Class Name"
                                required
                            />
                        </label>

                        <label className="block mt-4 text-sm">
                            <span className="text-gray-700 dark:text-gray-400">Class Code</span>
                            <input
                                value={code}
                                onChange={(e) => setCode(e.target.value)}
                                className="block w-full px-4 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md dark:bg-gray-700 dark:text-gray-300 dark:border-gray-600 dark:focus:shadow-outline-gray focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
                                placeholder="Class Code"
                                required
                            />
                        </label>

                        <label className="block mt-4 text-sm">
                            <span className="text-gray-700 dark:text-gray-400">Subject</span>
                            <input
                                value={subject}
                                onChange={(e) => setSubject(e.target.value)}
                                className="block w-full px-4 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md dark:bg-gray-700 dark:text-gray-300 dark:border-gray-600 dark:focus:shadow-outline-gray focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
                                placeholder="Subject"
                                required
                            />
                        </label>

                        <label className="block mt-4 text-sm">
                            <span className="text-gray-700 dark:text-gray-400">Description</span>
                            <textarea
                                value={description}
                                onChange={(e) => setDescription(e.target.value)}
                                className="block w-full px-4 py-3 mt-1 text-gray-700 bg-gray-200 border-transparent rounded-md dark:bg-gray-700 dark:text-gray-300 dark:border-gray-600 dark:focus:shadow-outline-gray focus:border-gray-500 focus:outline-none focus:shadow-outline-gray"
                                placeholder="Description"
                                required
                            />

                        </label>

                        <div className="flex items-center justify-between mt-4">
                            <button
                                className="px-4 py-2 text-sm font-medium leading-5 text-white transition-colors duration-150 bg-blue-600 border border-transparent rounded-lg active:bg-blue-600 hover:bg-blue-700 focus:outline-none focus:shadow-outline-blue"
                                type="submit"
                            >
                                Create
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default CreateClass;