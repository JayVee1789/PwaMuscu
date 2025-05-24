using Blazored.LocalStorage;
using PwaMuscu.Models;

namespace PwaMuscu.Services
{
    public class PoidsService
    {
        private const string StorageKey = "poids_entries";
        private readonly ILocalStorageService _localStorage;

        public PoidsService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<PoidsEntry>> GetEntriesAsync()
        {
            return await _localStorage.GetItemAsync<List<PoidsEntry>>(StorageKey) ?? new();
        }

        public async Task AddEntryAsync(PoidsEntry entry)
        {
            var entries = await GetEntriesAsync();
            entries.Add(entry);
            await _localStorage.SetItemAsync(StorageKey, entries);
        }
        public async Task RemoveEntryAsync(string exercice, DateTime date)
        {
            var entries = await GetEntriesAsync();
            entries.RemoveAll(e => e.Exercice == exercice && e.Date.Date == date.Date);
            await _localStorage.SetItemAsync(StorageKey, entries);
        }

        public async Task RemoveExerciceAsync(string exercice)
        {
            var entries = await GetEntriesAsync();
            entries.RemoveAll(e => e.Exercice == exercice);
            await _localStorage.SetItemAsync(StorageKey, entries);
        }

        public async Task RemoveDateAsync(DateTime date)
        {
            var entries = await GetEntriesAsync();
            entries.RemoveAll(e => e.Date.Date == date.Date);
            await _localStorage.SetItemAsync(StorageKey, entries);
        }
        private const string ExerciseKey = "exercise_list";

        public async Task<List<string>> GetExercicesAsync()
        {
            return await _localStorage.GetItemAsync<List<string>>(ExerciseKey) ?? new List<string> { "Développé couché", "Dips", "Squat" };
        }

        public async Task SaveExercicesAsync(List<string> list)
        {
            await _localStorage.SetItemAsync(ExerciseKey, list);
        }

    }
}
